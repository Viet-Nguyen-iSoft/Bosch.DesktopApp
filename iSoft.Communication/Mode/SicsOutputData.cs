using iSoft.Communication.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Communication.EnumCommunication;

namespace iSoft.Communication.Mode
{
  public class SicsOutputData
  {
    public double? IndicatedWeight;
    public double? TareWeight;
    public UnitOfWeight Unit;
    public ActiveWeighingStatus ActiveWeighingStatus { get; set; }
    public EnumValueWeightType EValueWeightType { get; set; }

    public static SicsOutputData sicsOutputData { get; set; } = new SicsOutputData();
    public static SicsOutputData? Decode(string message)
    {
      try
      {
        if (string.IsNullOrEmpty(message)) return null;
        Debug.WriteLine("Data Src: " + message);
        //SicsOutputData sicsOutputData = new SicsOutputData();
        //sicsOutputData = new SicsOutputData();
        string[] parts = message.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

       // if (parts.Length < 2) return null;
        string key = parts[0].Replace("\r", "").Replace("\n", "");

        if (key == "S")
        {
          sicsOutputData.EValueWeightType = EnumValueWeightType.Net;
          if (parts[1] == "S")
          {
            sicsOutputData.IndicatedWeight = double.Parse(parts[2]);
            sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Stable;
            if (parts[3].Trim() == "kg")
            {
              sicsOutputData.Unit = UnitOfWeight.Kilograms;
            }
          }
          else if (parts[1] == "D")
          {
            sicsOutputData.IndicatedWeight = double.Parse(parts[2]);
            sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Motion;
            if (parts[3].Trim() == "kg")
            {
              sicsOutputData.Unit = UnitOfWeight.Kilograms;
            }
          }
          else if (parts[1] == "+")
          {
            sicsOutputData.IndicatedWeight = 0.0;
            sicsOutputData.TareWeight = 0.0;
            sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Overload;
            sicsOutputData.Unit = UnitOfWeight.None;
          }
          else if (parts[1] == "-")
          {
            sicsOutputData.IndicatedWeight = 0.0;
            sicsOutputData.TareWeight = 0.0;
            sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Underload;
            sicsOutputData.Unit = UnitOfWeight.None;
          }
        }
        else
        {
         

          
        }

        Debug.WriteLine("Data: " + message);
        if (key == "TA")
        {
          if (parts.Length>=3)
          {
            sicsOutputData.EValueWeightType = EnumValueWeightType.Tare;
            sicsOutputData.TareWeight = double.Parse(parts[2]);
          }
         
        }
        else
        {
          if (!message.Contains("S"))
          {
            double T = 0.0;
            if (double.TryParse(message, out T) == true)
            {
              sicsOutputData.EValueWeightType = EnumValueWeightType.Tare;
              sicsOutputData.TareWeight = T;
            }
          }
          
        }
        //else if (key == "TA")
        //{
        //  sicsOutputData.EValueWeightType = EnumValueWeightType.Tare;
        //  sicsOutputData.TareWeight = double.Parse(parts[2]);
        //}  

        //if (message.Contains("TA"))
        //{

        //}
        //if (parts[3].Contains("TA"))
        //{
        //  sicsOutputData.EValueWeightType = EnumValueWeightType.Tare;
        //  sicsOutputData.TareWeight = double.Parse(parts[5]);
        //}
        //for (int i = 0; i < parts.Count(); i++)
        //{
        //  if (parts[i].Contains("TA"))
        //  {
        //    sicsOutputData.EValueWeightType = EnumValueWeightType.Tare;
        //    sicsOutputData.TareWeight = double.Parse(parts[i+2]);

        //    break;
        //  }
        //}

        return sicsOutputData;
      }
      catch (Exception ex)
      {
        //TODO
        string a = ex.StackTrace;
        return null;
      }
    }


    //BK 
    //public static SicsOutputData? Decode(string message)
    //{
    //  try
    //  {
    //    if (string.IsNullOrEmpty(message)) return null;

    //    SicsOutputData sicsOutputData = new SicsOutputData();
    //    string[] parts = message.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

    //    if (parts.Length < 4) return null;
    //    string key = parts[0].Replace("\r", "").Replace("\n", "");

    //    if (key == "S")
    //    {
    //      sicsOutputData.EValueWeightType = EnumValueWeightType.Net;
    //      if (parts[1] == "S")
    //      {
    //        sicsOutputData.IndicatedWeight = double.Parse(parts[2]);
    //        sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Stable;
    //        if (parts[3].Trim() == "kg")
    //        {
    //          sicsOutputData.Unit = UnitOfWeight.Kilograms;
    //        }
    //      }
    //      else if (parts[1] == "D")
    //      {
    //        sicsOutputData.IndicatedWeight = double.Parse(parts[2]);
    //        sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Motion;
    //        if (parts[3].Trim() == "kg")
    //        {
    //          sicsOutputData.Unit = UnitOfWeight.Kilograms;
    //        }
    //      }
    //      else if (parts[1] == "+")
    //      {
    //        sicsOutputData.IndicatedWeight = 0.0;
    //        sicsOutputData.TareWeight = 0.0;
    //        sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Overload;
    //        sicsOutputData.Unit = UnitOfWeight.None;
    //      }
    //      else if (parts[1] == "-")
    //      {
    //        sicsOutputData.IndicatedWeight = 0.0;
    //        sicsOutputData.TareWeight = 0.0;
    //        sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Underload;
    //        sicsOutputData.Unit = UnitOfWeight.None;
    //      }
    //    }
    //    else if (key == "TA")
    //    {
    //      sicsOutputData.EValueWeightType = EnumValueWeightType.Tare;
    //      sicsOutputData.TareWeight = double.Parse(parts[2]);
    //    }

    //    return sicsOutputData;
    //  }
    //  catch (Exception ex)
    //  {
    //    //TODO
    //    string a = ex.StackTrace;
    //    return null;
    //  }
    //}


    //public static SicsOutputData? Decode(string message, eValueWeightType eValueWeightType)
    //{
    //  try
    //  {
    //    if (string.IsNullOrEmpty(message)) return null;

    //    SicsOutputData sicsOutputData = new SicsOutputData();
    //    sicsOutputData.EValueWeightType = eValueWeightType;
    //    string[] parts = message.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

    //    if (parts.Length < 4) return null;

    //    if (eValueWeightType == eValueWeightType.Tare)
    //    {
    //      sicsOutputData.TareWeight = double.Parse(parts[2]);
    //    }
    //    else if (eValueWeightType == eValueWeightType.Net)
    //    {
    //      if (parts[1] == "S")
    //      {
    //        sicsOutputData.IndicatedWeight = double.Parse(parts[2]);
    //        sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Stable;
    //        if (parts[3].Trim() == "kg")
    //        {
    //          sicsOutputData.Unit = UnitOfWeight.Kilograms;
    //        }
    //      }
    //      else if (parts[1] == "D")
    //      {
    //        sicsOutputData.IndicatedWeight = double.Parse(parts[2]);
    //        sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Motion;
    //        if (parts[3].Trim() == "kg")
    //        {
    //          sicsOutputData.Unit = UnitOfWeight.Kilograms;
    //        }
    //      }
    //      else if (parts[1] == "+")
    //      {
    //        sicsOutputData.IndicatedWeight = 0.0;
    //        sicsOutputData.TareWeight = 0.0;
    //        sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Overload;
    //        sicsOutputData.Unit = UnitOfWeight.None;
    //      }
    //      else if (parts[1] == "-")
    //      {
    //        sicsOutputData.IndicatedWeight = 0.0;
    //        sicsOutputData.TareWeight = 0.0;
    //        sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Underload;
    //        sicsOutputData.Unit = UnitOfWeight.None;
    //      }
    //    }
    //    return sicsOutputData;
    //  }
    //  catch (Exception ex)
    //  {
    //    //TODO
    //    string a = ex.StackTrace;
    //    return null;
    //  }

    //}
  }
}
