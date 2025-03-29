function mod_artnow_caravan_check_succeed()
{
    with (owner)
    {
        var _timestamp = scr_npc_get_global_info("verrenCaravanIdentifyCD")
        if (_timestamp == 0)
        {
            _timestamp = scr_timeGetTimestamp()
            scr_npc_set_global_info("verrenCaravanIdentifyCD", _timestamp)
        }

        var _hoursPassed = scr_timeGetPassed(_timestamp, 2)
        var _items = mod_artknow_get_stash_unidentified()
        var _num = ds_list_size(_items)
        var _count = floor(_hoursPassed / 24)
        var _success = 0

        while (_count != 0 && _num != 0)
        {
            _count--
            _num--
            if scr_chance_value(global.caravan_chance_identify)
                _success++
        }

        scr_npc_set_global_info("verrenCaravanIdentifyCD", scr_timeGetTimestamp())

        if _success > 0
        {
            scr_npc_set_global_info("verrenCaravanIdentifyNumber", _success)
            return true
        }
        else
        {
            return false
        }

        ds_list_destroy(_items)
    }
}