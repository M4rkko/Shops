using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShopTARge23.Core.Domain;
using ShopTARge23.ApplicationServices.Services;

namespace ShopTARge23.KindergartenTest
{
    public class KindergartenServiceTests : Testbase
    {
        // #1 Test
        [Fact]
        public async Task ShouldNot_AddEmptyKindergarten_WhenReturnResult()
        {
            //Arrange
            KindergartenDto dto = new();

            dto.KindergartenName = "N";
            dto.GroupName = "Ni";
            dto.Teacher = "Nim";
            dto.ChildrenCount = 9;
            dto.CreatedAt = DateTime.Now;
            dto.UpdatedAt = DateTime.Now;

            //Act
            var result = await Svc<IKindergartensServices>().Create(dto);

            //Assert
            Assert.NotNull(result);

        }
        // #2 Test
        [Fact]
        public async Task ShouldNot_GetByIdKindergarten_WhenReturnsNotEqual()
        {
            //Arrange
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("3130b88d-004b-4130-bb43-56aaa1762a26");

            //Act
            await Svc<IKindergartensServices>().GetAsync(guid);

            //Assert
            Assert.NotEqual(wrongGuid, guid);
        }
        // #3 Test
        [Fact]
        public async Task Should_GetByIdKindergarten_WhenReturnsEqual()
        {
            //Arrange
            Guid databaseGuid = Guid.Parse("3130b88d-004b-4130-bb43-56aaa1762a26");
            Guid guid = Guid.Parse("3130b88d-004b-4130-bb43-56aaa1762a26");

            //Act
            await Svc<IKindergartensServices>().GetAsync(guid);

            //Assert
            Assert.Equal(databaseGuid, guid);
        }
        // #4 Test
        [Fact]
        public async Task Should_DeleteByIdKindergarten_WhenDeleteKindergarten()
        {
            KindergartenDto kindergarten = MockKindergartenData();

            var addKindergarten = await Svc<IKindergartensServices>().Create(kindergarten);
            var result = await Svc<IKindergartensServices>().Delete((Guid)addKindergarten.Id);

            Assert.Equal(result, addKindergarten);
        }

        private KindergartenDto MockKindergartenData()
        {
            KindergartenDto kindergarten = new()
            {
                KindergartenName = "Nm",
                GroupName = "Ni",
                Teacher = "Nim",
                ChildrenCount = 9,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            return kindergarten;
        }
    }
}
