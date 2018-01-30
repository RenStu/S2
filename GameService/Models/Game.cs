using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public int? FriendId { get; set; }

        public bool IsValid
        {
            get { return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true); }
        }

        public bool Update(Game game)
        {
            if (this.Id == game.Id)
            {
                this.Name = game.Name;
                this.FriendId = game.FriendId;

                return true;
            }
            return false;
        }

        public bool Devolve(Game game)
        {
            if (this.Id == game.Id)
            {
                this.Name = game.Name;
                this.FriendId = null;

                return true;
            }
            return false;
        }
    }
}
