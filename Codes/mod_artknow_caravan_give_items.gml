function mod_artknow_caravan_give_items()
{
    with (owner)
    {
        var _items = array_shuffle(mod_artknow_get_stash_unidentified())
        var _num = scr_npc_get_global_info("verrenCaravanIdentifyNumber")

        with (scr_guiCreateContainer(global.guiBaseContainerVisible, o_reward_container))
        {
            for (var i = 0; i < _num; i++)
            {
                var _itm = _items[i]
                ds_map_replace(ds_list_find_value(_itm.item, 1), "identified", true)
                ds_list_add(loot_list, _itm.item)
            }

            scr_loadContainerContent(loot_list, object_index, noone, id, true, false)
        }

        scr_npc_set_global_info("verrenCaravanIdentifyNumber", 0)

        var item_count = array_length(_items);


        // -- 1) Sorting:
        //    First sort by stash_list in ascending order,
        //    then sort by index in descending order (when they share the same stash_list).
        for (var i = 0; i < item_count - 1; i++)
        {
            for (var j = 0; j < item_count - 1 - i; j++)
            {
                var a = _items[j];
                var b = _items[j + 1];

                // If a.stash_list > b.stash_list, swap them.
                // If they have the same stash_list but a.index < b.index, swap them too.
                if ((a.stash_list > b.stash_list)
                ||  (a.stash_list == b.stash_list && a.index < b.index))
                {
                    _items[j]     = b;
                    _items[j + 1] = a;
                }
            }
        }

        // -- 2) Delete in order
        for (var i = 0; i < item_count; i++)
        {
            var itm = _items[i];
            ds_list_delete(itm.stash_list, itm.index);
        }
    }
}
