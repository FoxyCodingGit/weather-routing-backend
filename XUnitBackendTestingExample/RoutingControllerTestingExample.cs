using System;
using System.Collections.Generic;
using WeatherRoutingBackend.Controllers;
using WeatherRoutingBackend.Model.Route;
using Xunit;

namespace XUnitBackendTestingExample
{
    public class RoutingControllerTestingExample
    {
        [Fact]
        public void GenerateUsefulRouteResponse_EnterOneValidRoute_ConvertedCorrectly()
        {
            // Arrange
            RouteResponse OldRouteFormat = new RouteResponse { Routes = new List<Route> { GetRoute1() } };

            // Act
            List<UsefulRouteResponse> actualResult = RoutingController.GenerateUsefulRouteResponse(OldRouteFormat);

            // Assert

            Assert.Equal(Route1ExpectedOutcome[0].TravelTimeInSeconds, actualResult[0].TravelTimeInSeconds);
            Assert.Equal(Route1ExpectedOutcome[0].Distance, actualResult[0].Distance);

            for (int focusedPointIndex = 0; focusedPointIndex < Route1ExpectedOutcome[0].Points.Count; focusedPointIndex++)
            {
                Assert.Equal(Route1ExpectedOutcome[0].Points[focusedPointIndex].Latitude, actualResult[0].Points[focusedPointIndex].Latitude);
                Assert.Equal(Route1ExpectedOutcome[0].Points[focusedPointIndex].Longitude, actualResult[0].Points[focusedPointIndex].Longitude);
            }
        }

        [Fact]
        public void GenerateUsefulRouteResponse_MultipleRoutes_AllRoutesConvertedCorrectly()
        {
            // Arrange
            RouteResponse OldRouteFormat = new RouteResponse { Routes = new List<Route> { GetRoute1(), GetRoute2() } };

            // Act
            List<UsefulRouteResponse> actualResult = RoutingController.GenerateUsefulRouteResponse(OldRouteFormat);

            // Assert
            Assert.Equal(2, actualResult.Count);

            for (int focusedResponseIndex = 0; focusedResponseIndex < Route1And2ExpectedOutcome.Count; focusedResponseIndex++)
            {
                Assert.Equal(Route1And2ExpectedOutcome[focusedResponseIndex].TravelTimeInSeconds, actualResult[focusedResponseIndex].TravelTimeInSeconds);
                Assert.Equal(Route1And2ExpectedOutcome[focusedResponseIndex].Distance, actualResult[focusedResponseIndex].Distance);

                for (int focusedPointIndex = 0; focusedPointIndex < actualResult[0].Points.Count; focusedPointIndex++)
                {
                    Assert.Equal(Route1And2ExpectedOutcome[focusedResponseIndex].Points[focusedPointIndex].Latitude, actualResult[focusedResponseIndex].Points[focusedPointIndex].Latitude);
                    Assert.Equal(Route1And2ExpectedOutcome[focusedResponseIndex].Points[focusedPointIndex].Longitude, actualResult[focusedResponseIndex].Points[focusedPointIndex].Longitude);
                }
            }
        }

        private Route GetRoute1()
        {
            return new Route
            {
                Summary = new Summary
                {
                    LengthInMeters = 2000,
                    TravelTimeInSeconds = 3000,
                    DepartureTime = new DateTime(200),
                    ArrivalTime = new DateTime(300)
                },
                Legs = new List<Leg>
                {
                    new Leg
                    {
                        Summary = new Summary
                        {
                            LengthInMeters = 6,
                            TravelTimeInSeconds = 7,
                            DepartureTime = new DateTime(500),
                            ArrivalTime = new DateTime(600)
                        },
                        Points = new List<Point>
                        {
                            new Point { Latitude = 1, Longitude = 2 },
                            new Point { Latitude = 3, Longitude = 4 },
                            new Point { Latitude = 5, Longitude = 6 },
                            new Point { Latitude = 7, Longitude = 8 },
                            new Point { Latitude = 9, Longitude = 10 }
                        }
                    }
                }
            };
        }

        private Route GetRoute2()
        {
            return new Route
            {
                Summary = new Summary
                {
                    LengthInMeters = 20000,
                    TravelTimeInSeconds = 30000,
                    DepartureTime = new DateTime(2000),
                    ArrivalTime = new DateTime(3000)
                },
                Legs = new List<Leg>
                {
                    new Leg
                    {
                        Summary = new Summary
                        {
                            LengthInMeters = 60,
                            TravelTimeInSeconds = 70,
                            DepartureTime = new DateTime(5000),
                            ArrivalTime = new DateTime(6000)
                        },
                        Points = new List<Point>
                        {
                            new Point { Latitude = 21, Longitude = 22 },
                            new Point { Latitude = 23, Longitude = 24 },
                            new Point { Latitude = 25, Longitude = 26 },
                            new Point { Latitude = 27, Longitude = 28 },
                            new Point { Latitude = 29, Longitude = 30 }
                        }
                    }
                }
            };
        }



        private readonly List<UsefulRouteResponse> Route1ExpectedOutcome = new List<UsefulRouteResponse>
        {
            new UsefulRouteResponse {
                Points = new List<Point>
                {
                    new Point { Latitude = 1, Longitude = 2 },
                    new Point { Latitude = 3, Longitude = 4 },
                    new Point { Latitude = 5, Longitude = 6 },
                    new Point { Latitude = 7, Longitude = 8 },
                    new Point { Latitude = 9, Longitude = 10 }
                },
                TravelTimeInSeconds = 3000,
                Distance = 2000
            }
        };

        private readonly List<UsefulRouteResponse> Route1And2ExpectedOutcome =
            new List<UsefulRouteResponse>
            {
                new UsefulRouteResponse {
                    Points = new List<Point>
                    {
                        new Point { Latitude = 1, Longitude = 2 },
                        new Point { Latitude = 3, Longitude = 4 },
                        new Point { Latitude = 5, Longitude = 6 },
                        new Point { Latitude = 7, Longitude = 8 },
                        new Point { Latitude = 9, Longitude = 10 }
                    },
                    TravelTimeInSeconds = 3000,
                    Distance = 2000
                },

                new UsefulRouteResponse {
                    Points = new List<Point>
                    {
                        new Point { Latitude = 21, Longitude = 22 },
                        new Point { Latitude = 23, Longitude = 24 },
                        new Point { Latitude = 25, Longitude = 26 },
                        new Point { Latitude = 27, Longitude = 28 },
                        new Point { Latitude = 29, Longitude = 30 }
                    },
                    TravelTimeInSeconds = 30000,
                    Distance = 20000
                },
            };
    }
}
