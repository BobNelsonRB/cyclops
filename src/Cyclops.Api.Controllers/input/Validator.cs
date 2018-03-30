using System;
using System.Collections.Generic;
using System.Text;
using Cyclops.Model;

namespace Cyclops.Api.Input
{
    public static class Validator
    {
        internal static bool TryValidate(NoteInput input, out Note model, out string errorMessage)
        {
            bool b = true;
            errorMessage = String.Empty;
            model = new Note();
            b = b && !String.IsNullOrWhiteSpace(input.Id);
            b = b && !String.IsNullOrWhiteSpace(input.Display);
            b = b && !String.IsNullOrWhiteSpace(input.Body);
            b = b && !String.IsNullOrWhiteSpace(input.Disposition);
            b = b && !String.IsNullOrWhiteSpace(input.Tags);
            b = b && !String.IsNullOrWhiteSpace(input.Username);
            if (b)
            {
                model.Id = input.Id;
                model.Display = input.Display;
                model.Body = input.Body;
                model.Disposition = input.Disposition;
                model.CreatedBy = input.Username;
                model.ModifiedBy = input.Username;
                model.Tags = new List<string>(input.Tags.Split(new char[] { ',', ';' },StringSplitOptions.RemoveEmptyEntries));
            }
            return b;
        }
    }
}
