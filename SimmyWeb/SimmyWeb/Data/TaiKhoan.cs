using System;
using System.Collections.Generic;

namespace SimmyWeb.Data;

public partial class TaiKhoan
{
    public int MaTaiKhoan { get; set; }

    public string Email { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public bool TrangThai { get; set; }

    public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
}
