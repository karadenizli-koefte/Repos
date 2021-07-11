using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkishPoliticalViewsPredictor.Model
{
    /* 
    Cinsiyet : Sex Feature
    Yas : Age Feature
    Bolge : Areas inhabited in Turkey
    Egitim : Education Level
    (Soru = Question) (Questions include Turkey)
    Soru1/Question1: Do you think our Economic Status is good?
    Soru2/Question2: Need Reform in Education?
    Soru3/Question3: Resolve Privatization Are You?
    Soru4/Question4: Should the state use a penalty like death penalty for certain crimes?
    Soru5/Question5: Do you find our journalists neutral enough?
    Soru6/Question6: From 22:00 am Then Are You Supporting the Prohibition to Buy Drinks?
    Soru7/Question7: Do You Want to Live in a Secular State?
    Soru8/Question8: Are you supporting the abortion ban?
    Soru9/Question9: Do you think that the extraordinary state(Ohal) restricts Freedoms?
    Soru10/Question10: Would you like a new part of the parliament to enter?
    Parti : Political View
    */

    public class PoliticalPersonRaw
    {
        public PoliticalPersonRaw()
        {

        }

        public PoliticalPersonRaw(string sex, string age, string area, string educationLevel, string question1, string question2, string question3, string question4, string question5, string question6, string question7, string question8, string question9, string question10)
        {
            Sex = sex;
            Age = age;
            Area = area;
            EducationLevel = educationLevel;
            Question1 = question1;
            Question2 = question2;
            Question3 = question3;
            Question4 = question4;
            Question5 = question5;
            Question6 = question6;
            Question7 = question7;
            Question8 = question8;
            Question9 = question9;
            Question10 = question10;
        }

        [LoadColumn(0)]
        public string TimeStamp { get; set; }
        [LoadColumn(1)]
        public string Sex { get; set; }
        [LoadColumn(2)]
        public string Age { get; set; }
        [LoadColumn(3)]
        public string Area { get; set; }
        [LoadColumn(4)]
        public string EducationLevel { get; set; }
        [LoadColumn(5)]
        public string Question1 { get; set; }
        [LoadColumn(6)]
        public string Question2 { get; set; }
        [LoadColumn(7)]
        public string Question3 { get; set; }
        [LoadColumn(8)]
        public string Question4 { get; set; }
        [LoadColumn(9)]
        public string Question5 { get; set; }
        [LoadColumn(10)]
        public string Question6 { get; set; }
        [LoadColumn(11)]
        public string Question7 { get; set; }
        [LoadColumn(12)]
        public string Question8 { get; set; }
        [LoadColumn(13)]
        public string Question9 { get; set; }
        [LoadColumn(14)]
        public string Question10 { get; set; }
        [LoadColumn(15)]
        public string Party { get; set; }
    }
    public class PoliticalPerson {
        [LoadColumn(0)]
        public Sex Sex { get; set; }
        [LoadColumn(1)]
        public Age Age { get; set; }
        [LoadColumn(2)]
        public Area Area { get; set; }
        [LoadColumn(3)]
        public EducationLevel EducationLevel { get; set; }
        [LoadColumn(4)]
        public bool Question1{ get; set; }
        [LoadColumn(5)]
        public bool Question2{ get; set; }
        [LoadColumn(6)]
        public bool Question3{ get; set; }
        [LoadColumn(7)]
        public bool Question4{ get; set; }
        [LoadColumn(8)]
        public bool Question5{ get; set; }
        [LoadColumn(9)]
        public bool Question6{ get; set; }
        [LoadColumn(10)]
        public bool Question7{ get; set; }
        [LoadColumn(11)]
        public bool Question8{ get; set; }
        [LoadColumn(12)]
        public bool Question9 { get; set; }
        [LoadColumn(13)]
        public bool Question10 { get; set; }
        [LoadColumn(14)]
        public Party Party { get; set; }
    };
}
