using System;
using System.Collections.Generic;

namespace SimmyWeb.Data;

public partial class ThanhToan
{
    public string MaThanhToan { get; set; } = null!;

    public string MaDonHang { get; set; } = null!;

    public string PhuongThucThanhToan { get; set; } = null!;

    public DateTime? NgayThanhToan { get; set; }

    public double TongTien { get; set; }

    public bool TrangThaiThanhToan { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual DonHang MaDonHangNavigation { get; set; } = null!;
}
