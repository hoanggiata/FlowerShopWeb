using System;
using System.Collections.Generic;

namespace AeFLOWER.Models;

public partial class Comment
{
    public string IdComment { get; set; } = null!;

    public string? CommentContent { get; set; }

    public DateTime? CommentTime { get; set; }

    public string? IdUser { get; set; }

    public string? IdFlower { get; set; }

    public int? StarRating { get; set; }

    public virtual Product? IdFlowerNavigation { get; set; }

    public virtual Account? IdUserNavigation { get; set; }
}
