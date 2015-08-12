﻿namespace TechSupport.Common.DataAnnotations
{
    using System;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ExcludeFromExcelAttribute : Attribute
    {
    }
}