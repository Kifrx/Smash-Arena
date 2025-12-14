using System;
using System.Collections.Generic;

namespace SmashArenaAPI.Models;

public partial class Court
{
    public string CourtId { get; set; } = null!;

    public string? NamaCourt { get; set; }

    public string? JenisLantai { get; set; }

    public string? Fasilitas { get; set; }

    public decimal? HargaPerJam { get; set; }

    public string? GambarUrl { get; set; }

    public bool? StatusAktif { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
