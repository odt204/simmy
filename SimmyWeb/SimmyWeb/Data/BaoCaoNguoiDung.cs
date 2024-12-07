using System;
using System.Collections.Generic;

namespace SimmyWeb.Data;

public partial class BaoCaoNguoiDung
{
    public int MaBaoCao { get; set; }

    public int MaNguoiDung { get; set; }

    public string? PhanHoi { get; set; }

    public DateTime? NgayTao { get; set; }

    public int? TrangThai { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual ICollection<PhanHoiBaoCao> PhanHoiBaoCaos { get; set; } = new List<PhanHoiBaoCao>();
}
