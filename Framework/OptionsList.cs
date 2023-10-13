using System;
using System.Collections.Generic;

namespace Rivel.Framework
{

    /// <summary>
    /// This class is used to create a list of options and show them to the user
    /// </summary>
    internal class OptionsList
    {
        List<Option> Options { set; get; }

        /// <summary>
        /// Gets or sets the fallback that will be called when the user enters an invalid option.
        /// </summary>
        Action FallBack { get; set; }

        public OptionsList(List<Option> options, Action fallback)
        {
            Option.Count = 0; // to prevent the count from incrementing every time we create a new instance of OptionsList
            FallBack = fallback;
            Options = options;
            Show();
            SelectOption();
        }
        public void Show()
        {
            Options.ForEach(option => Helper.Print($"{option.Id}. {option.Name}"));
        }

        // validate the user input and call the fallback if the input is invalid
        public void SelectOption()
        {
            try
            {
                int optionId = Helper.GetUserInputAsInt("Enter Option Id: ");
                Option selectedOption = Options.Find(option => option.Id == optionId) ?? throw new Exception();
                selectedOption.FallBack();
            }
            catch
            {
                FallBack();
            }
        }
    }

    /// <summary>
    /// This class is used to create an option object
    /// </summary>
    class Option
    {
        public static int Count;
        public int Id { get; set; }
        public string Name { get; set; }
        public Action FallBack { get; set; }

        public Option(string name, Action fallBack)
        {
            Count++;
            Id = Count;
            Name = name;
            FallBack = fallBack;
        }
    }
}
