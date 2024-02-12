using Microsoft.EntityFrameworkCore;
using Notebook.DAL;
using Notebook.DAL.Repository;
using Notebook.Domain.Entity;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Notebook.Tests
{
    [TestFixture]
    public class PersonRepositoryTests
    {
        private PersonRepository _repository;

        [SetUp]
        public void Setup()
        {
            // ����� �� ������ ������� ��������� ������ ����������� � ��������� ��������� ���� ������
            _repository = new PersonRepository(AppDBContext.Create());
        }

        [Test]
        public async Task Create_ValidPerson_ReturnsTrue()
        {
            // Arrange
            var person = new Person { /* ����� ������� �������� ������� */ };

            // Act
            var result = await _repository.Create(person);

            // Assert
            Assert.IsTrue(result);
            // �������������� ��������, ���� ����������
        }
    }
}