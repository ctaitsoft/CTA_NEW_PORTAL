//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CTA_NEW_PORTAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccountInfo
    {
        public int AccID { get; set; }
        public short LineID { get; set; }
        public Nullable<byte> InfoType { get; set; }
        public string InfoData { get; set; }
        public bool Cancelled { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public Nullable<int> EnterUser { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<System.TimeSpan> EnterTime { get; set; }
        public Nullable<int> ModUser { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public Nullable<System.TimeSpan> ModTime { get; set; }
    }
}