using System;
using System.Collections.Generic;

namespace SimmyWeb.Data;

public partial class PhanHoiBaoCao
{
    public int MaPhanHoi { get; set; }

    public int MaBaoCao { get; set; }

    public int MaNguoiDung { get; set; }

    public string NoiDung { get; set; } = null!;

    public DateTime? NgayPhanHoi { get; set; }

    public bool NguoiTraLoi { get; set; }

    public virtual BaoCaoNguoiDung MaBaoCaoNavigation { get; set; } = null!;

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
