function mod_push_identify_dialog()
{
    var _story_adress_array = argument0
    var _answer = argument1
    var _temp = scr_add_answer_to_dialog("askVerrenIdentify")
    with (o_inv_slot)
    {
        if (!scr_dsMapFindValue(data, "identified", true))
        {
            array_insert(_answer, 0, _temp[0])
            array_insert(_story_adress_array, 0, argument2)
            break
        }
    }
}