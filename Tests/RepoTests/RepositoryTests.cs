﻿using EFCoreSugar;
using EFCoreSugar.Filters;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Tests.FakeDatabase;
using Tests.FakeEntities;
using Tests.FilterTestGoup;
using Xunit;

namespace Tests.RepoTests
{
    public class RepositoryTests : BaseTest
    {

        [Fact]
        public void ServiceCollectionExtensionTests()
        {
            var services = new ServiceCollection() as IServiceCollection;
            //we just need this so we can resolve the repo;
            services.AddScoped<TestDbContext>();
            services.RegisterBaseRepositories();
            services.RegisterRepositoryGroups();
            var servicesProvider = services.BuildServiceProvider();

            var repo = servicesProvider.GetService<FakeRepo>();
            var irepo = servicesProvider.GetService<IFakeRepo>();

            repo.Should().NotBeNull();
            irepo.Should().NotBeNull();

            irepo.UserGroup.GetUsersBySpecialMagic("stuff").Should().Be(1);
            repo.UserGroup.GetUsersBySpecialMagic("stuff").Should().Be(1);
            repo.ConcreteUserGroup.GetUsersBySpecialMagic("stuff").Should().Be(1);
        }
    }
}
