using System;
using System.Collections.Generic;

namespace SmashArenaAPI.Models;

public partial class Booking
{
    public string BookingId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string CourtId { get; set; } = null!;

    public DateOnly? TanggalMain { get; set; }

    public TimeOnly? JamMulai { get; set; }

    public TimeOnly? JamSelesai { get; set; }

    public decimal? TotalHarga { get; set; }

    public string? StatusBooking { get; set; }

    public DateTime? WaktuDibuat { get; set; }

    public virtual Court Court { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
