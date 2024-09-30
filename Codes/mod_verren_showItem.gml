function mod_verren_showItem()
{
    var _story_adress_array = argument0
    var _answer = argument1

    story_text = "askVerrenIdentify_showItem"
    _story_adress_array = [argument2, argument3]
    _answer = scr_add_answer_to_dialog("askVerrenIdentify_showItem_pc", "askVerrenIdentify_showItem_abort")

    return [_story_adress_array, _answer];
}
