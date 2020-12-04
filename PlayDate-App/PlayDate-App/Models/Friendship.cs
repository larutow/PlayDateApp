using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Models
{
    public class Friendship
    {
        [Key]
        public int FriendshipId { get; set; }
        [ForeignKey("Parent")]
        public int ParentOneId { get; set; }
        public Parent ParentOne { get; set; }
        [ForeignKey("Parent")]
        public int ParentTwoId { get; set; }
        public Parent ParentTwo { get; set; }
        public bool FriendshipRequest { get; set; } 
        public bool FriendshipConfirmed { get; set; }
        


    }
}
