﻿using LaYumba.Functional;
using System;

namespace FPLibrary
{
    public static partial class FP
    {
        //double AverageYearsWorkedAtTheCompany(List<Employee> employees) => // your implementation here...
        public class Employee
        {
            public Employee(string id, Option<WorkPermit> workPermit, DateTime joinedOn, Option<DateTime> leftOn)
            {
                Id = id;
                WorkPermit = workPermit;
                JoinedOn = joinedOn;
                LeftOn = leftOn;
            }

            public string Id { get; set; }
            public Option<WorkPermit> WorkPermit { get; set; }
            public DateTime JoinedOn { get; }
            public Option<DateTime> LeftOn { get; }
        }
    }
}
