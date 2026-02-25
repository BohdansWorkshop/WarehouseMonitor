using FluentAssertions;
using MediatR;
using Moq;
using WarehouseMonitor.Application.Common;
using WarehouseMonitor.Application.Common.Interfaces;
using WarehouseMonitor.Application.ShipmentUnits.Commands.Deliver;
using WarehouseMonitor.Domain.Entities;
using WarehouseMonitor.Domain.Enums;
using WarehouseMonitor.Domain.Events;

public class ShipmentUnitTests
{
    [Fact]
    public void NewShipmentUnit_Should_Have_CreatedStatus()
    {
        var productId = Guid.NewGuid();
        var shipment = new ShipmentUnit("TRK123", 10, 5, productId);

        shipment.Status.Should().Be(ShipmentStatus.Created);
        shipment.TrackingNumber.Should().Be("TRK123");
        shipment.Weight.Should().Be(10);
        shipment.Volume.Should().Be(5);
        shipment.ProductId.Should().Be(productId);
    }

    [Fact]
    public void Deliver_Should_Raise_ShipmentUnitDeliveredEvent()
    {
        var productId = Guid.NewGuid();
        var shipment = new ShipmentUnit("TRK123", 10, 5, productId);

        shipment.ReceiveAtWarehouse(Guid.NewGuid());
        shipment.Status.Should().Be(ShipmentStatus.Received);

        shipment.Ship(Guid.NewGuid());
        shipment.Status.Should().Be(ShipmentStatus.InTransit);
        shipment.DomainEvents.Should().ContainSingle(e => e is ShipmentUnitShipEvent);

        shipment.Deliver();
        shipment.Status.Should().Be(ShipmentStatus.Delivered);

        shipment.DomainEvents.Should().ContainSingle(e => e is ShipmentUnitDeliveredEvent);
    }

    [Fact]
    public void Deliver_From_Created_Should_Throw()
    {
        var shipment = new ShipmentUnit("TRK", 10, 5, Guid.NewGuid());

        Action act = () => shipment.Deliver();

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Ship_Without_Receiving_Should_Throw()
    {
        var shipment = new ShipmentUnit("TRK", 10, 5, Guid.NewGuid());

        Action act = () => shipment.Ship(Guid.NewGuid());

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ReceiveTwice_Should_Throw()
    {
        var shipment = new ShipmentUnit("TRK", 10, 5, Guid.NewGuid());
        shipment.ReceiveAtWarehouse(Guid.NewGuid());

        Action act = () => shipment.ReceiveAtWarehouse(Guid.NewGuid());

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Full_Lifecycle_Should_Work()
    {
        var shipment = new ShipmentUnit("TRK", 10, 5, Guid.NewGuid());

        shipment.Status.Should().Be(ShipmentStatus.Created);

        shipment.ReceiveAtWarehouse(Guid.NewGuid());
        shipment.Status.Should().Be(ShipmentStatus.Received);

        shipment.Ship(Guid.NewGuid());
        shipment.Status.Should().Be(ShipmentStatus.InTransit);

        shipment.Deliver();
        shipment.Status.Should().Be(ShipmentStatus.Delivered);
    }

    [Fact]
    public async Task Handle_Should_DeliverShipmentAndPublishEvent()
    {
        var shipment = new ShipmentUnit("TRK123", 10, 5, Guid.NewGuid());
        shipment.Status = ShipmentStatus.InTransit;

        var dbMock = new Mock<IApplicationDbContext>();
        dbMock.Setup(db => db.ShipmentUnits.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync(shipment);

        var mediatorMock = new Mock<IMediator>();
        var handler = new DeliverShipmentUnitCommandHandler(dbMock.Object, mediatorMock.Object);

        var result = await handler.Handle(new DeliverShipmentUnitCommand(shipment.Id), CancellationToken.None);
        result.Should().BeTrue();
        shipment.Status.Should().Be(ShipmentStatus.Delivered);

        mediatorMock.Verify(m =>
            m.Publish(It.Is<DomainEventNotification>(n =>
                n.Event is ShipmentUnitDeliveredEvent),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handler_Should_Not_Publish_Domain_Events_Directly()
    {
        var shipment = new ShipmentUnit("RTN123", 10, 5, Guid.NewGuid());
        shipment.Status = ShipmentStatus.InTransit;

        var dbMock = new Mock<IApplicationDbContext>();
        dbMock.Setup(db => db.ShipmentUnits.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>())).ReturnsAsync(shipment);

        var mediatorMock = new Mock<IMediator>();

        var handler = new DeliverShipmentUnitCommandHandler(dbMock.Object, mediatorMock.Object);

        await handler.Handle(new DeliverShipmentUnitCommand(shipment.Id), CancellationToken.None);

        mediatorMock.Verify(x =>
        x.Publish(It.IsAny<ShipmentUnitDeliveredEvent>(),
        It.IsAny<CancellationToken>()),
        Times.Never);
    }

}