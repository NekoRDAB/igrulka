// using NUnit.Framework;
// using UnityEngine;
//
// namespace Assets.Scripts
// {
//     public class MapController_Tests
//     {
//         
//     }
//     
//     [TestFixture]
//     public class InfiniteMapTests 
//     {
//         [Test]
//         public void TestShipInSegment()
//         {
//             // Arrange
//             var infiniteMap = new InfiniteMap();
//             var ownShip = new GameObject();
//             ownShip.transform.position = new Vector3(0, 0); // установить позицию ownShip в центр сегмента
//             infiniteMap.ownShip = ownShip;
//             infiniteMap.numSegments = 1;  // будем тестировать только первый сегмент
//
//             // Act
//             bool shipInSegment = infiniteMap.ShipInSegment(0);
//
//             // Assert
//             Assert.IsTrue(shipInSegment);
//         }
//     }
// }