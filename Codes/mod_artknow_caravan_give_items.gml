function mod_artknow_caravan_give_items()
{
    with (owner)
    {
        var _items = mod_artknow_get_stash_unidentified()
        ds_list_shuffle(_items)
        var _num = scr_npc_get_global_info("verrenCaravanIdentifyNumber")
        var _selected = []

        with (scr_guiCreateContainer(global.guiBaseContainerVisible, o_reward_container))
        {
            for (var i = 0; i < _num; i++)
            {
                var _itm = ds_list_find_value(_items, i)
                array_push(_selected, _itm)
                ds_map_replace(ds_list_find_value(ds_map_find_value(_itm, "item"), 1), "identified", true)
                ds_list_add(loot_list, ds_map_find_value(_itm, "item"))
            }

            scr_loadContainerContent(loot_list, object_index, noone, id, true, false)
        }

        scr_npc_set_global_info("verrenCaravanIdentifyNumber", 0)

        var _selected_count = array_length(_selected);

        // -- 1) Sorting:
        //    First sort by stash_list in ascending order,
        //    then sort by index in descending order (when they share the same stash_list).
        for (var i = 0; i < _selected_count - 1; i++)
        {
            for (var j = 0; j < _selected_count - 1 - i; j++)
            {
                var a = _selected[j];
                var b = _selected[j + 1];

                // If a.stash_list > b.stash_list, swap them.
                // If they have the same stash_list but a.index < b.index, swap them too.
                if ((ds_map_find_value(a, "stash_list") > ds_map_find_value(b, "stash_list"))
                ||  (ds_map_find_value(a, "stash_list") == ds_map_find_value(b, "stash_list")
                && ds_map_find_value(a, "index") < ds_map_find_value(a, "index")))
                {
                    _selected[j]     = b;
                    _selected[j + 1] = a;
                }
            }
        }

        // -- 2) Delete in order
        for (var i = 0; i < _selected_count; i++)
        {
            var itm = _selected[i];
            ds_list_delete(ds_map_find_value(itm, "stash_list"), ds_map_find_value(itm, "index"));
        }

        ds_list_destroy(_items)
    }
}
