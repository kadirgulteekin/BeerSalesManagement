﻿using Application.Abstractions.Messaging;
using Domain.Users;
using Infrastructure.Database;
using System.Reflection;
using Web.Api;

namespace BeerSalesManagerTest;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(User).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}