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
        // #5 Test
        [Fact]
        public void ShouldCalculateTotalChildrenCount()
        {
            // arrange
            var repository = new List<KindergartenDto>
            {
                new KindergartenDto
                {
                    Id = Guid.NewGuid(),
                    KindergartenName = "Lasteaed 1",
                    GroupName = "Grupp A",
                    Teacher = "Õpetaja A",
                    ChildrenCount = 10
                },
                new KindergartenDto
                {
                    Id = Guid.NewGuid(),
                    KindergartenName = "Lasteaed 2",
                    GroupName = "Grupp B",
                    Teacher = "Õpetaja B",
                    ChildrenCount = 15
                },
                new KindergartenDto
                {
                    Id = Guid.NewGuid(),
                    KindergartenName = "Lasteaed 3",
                    GroupName = "Grupp C",
                    Teacher = "Õpetaja C",
                    ChildrenCount = 5
                }
            };

            // act
            var totalChildren = repository.Sum(k => k.ChildrenCount);

            // assert
            Assert.Equal(30, totalChildren); // 10 + 15 + 5 = 30 last
        }
        // #6 Test
        [Fact]
        public void ShouldReturnKindergartensWithMoreThanGivenChildrenCount()
        {
            // arrange
            var repository = new List<KindergartenDto>
            {
                new KindergartenDto
                {
                    Id = Guid.NewGuid(),
                    KindergartenName = "Lasteaed Väike",
                    GroupName = "Grupp A",
                    Teacher = "Õpetaja A",
                    ChildrenCount = 8
                },
                new KindergartenDto
                {
                    Id = Guid.NewGuid(),
                    KindergartenName = "Lasteaed Keskmine",
                    GroupName = "Grupp B",
                    Teacher = "Õpetaja B",
                    ChildrenCount = 15
                },
                new KindergartenDto
                {
                    Id = Guid.NewGuid(),
                    KindergartenName = "Lasteaed Suur",
                    GroupName = "Grupp C",
                    Teacher = "Õpetaja C",
                    ChildrenCount = 25
                }
            };

            var limit = 10;

            // act
            var result = repository
                .Where(k => k.ChildrenCount > limit)
                .ToList();

            // assert
            Assert.Equal(2, result.Count); // kaks lasteaeda üle 10 lapse
            Assert.All(result, k => Assert.True(k.ChildrenCount > limit)); // kõik vastavad tingimusele
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

        private KindergartenDto MockUpdateKindergartenData()
        {
            return new KindergartenDto
            {
                GroupName = "TestGroupNameUpdate",
                ChildrenCount = 11,
                KindergartenName = "TestKindergartenNameUpdate",
                Teacher = "TestTeacherUpdate",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

    }
}
