using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.Users.Entities.Enums
{

    public enum ModifierRules
    {
        [Description("Servers must make a selection for this group")]
        Servers_must_make_a_selection_for_this_group = 1,

        [Description("This group is optional and is shown on add")]
        This_group_is_optional_and_is_shown_on_add,

        [Description("This group is optional and is not shown on add")]
        This_group_is_optional_and_is_not_shown_on_add,

        [Description("More than modifier can be chosen")]
        More_than_modifier_can_be_chosen,

        [Description("Only one modifier can be chosen")]
        Only_one_modifier_can_be_chosen

         
    }

    public static class ModifierRulesFormatter
    {
        public static string ToCustomFormat(this ModifierRules rule)
        {

            switch (rule)
            {
                case ModifierRules.Servers_must_make_a_selection_for_this_group:
                    return "Servers must make a selection for this group";

                case ModifierRules.This_group_is_optional_and_is_shown_on_add:
                    return "This group is optional and is shown on add";

                case ModifierRules.This_group_is_optional_and_is_not_shown_on_add:
                    return "This group is optional and is not shown on add";

                case ModifierRules.More_than_modifier_can_be_chosen:
                    return "More than modifier can be chosen";

                case ModifierRules.Only_one_modifier_can_be_chosen:
                    return "Only one modifier can be chosen";

                default:
                    return string.Empty;
            }

        }
    }


}
