using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Models
{
    public class Friendships
    {
        [Key]
        public int FriendshipsId { get; set; }
        [ForeignKey("Parent")]
        public int ParentOne { get; set; }
        [ForeignKey("Parent")]
        public int ParentTwo { get; set; }
        public bool FriendshipRequest { get; set; } 
        public bool FriendshipConfirmed { get; set; }
        


    }
}
