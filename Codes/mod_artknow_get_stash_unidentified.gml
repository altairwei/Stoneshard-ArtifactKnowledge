function mod_artknow_get_stash_unidentified()
{
    var _stash_array = [global.caravanStashDataList1, global.caravanStashDataList2, global.caravanStashDataList3, global.caravanStashDataList4];
    var _stash_array_size = array_length(_stash_array);

    var _unidentified_items = ds_list_create()
    for (var i = 0; i < _stash_array_size; i++)
    {
        var _stash_list = _stash_array[i]
        var _stash_list_size = ds_list_size(_stash_list)

        for (var j = 0; j < _stash_list_size; j++)
        {
            var _item = ds_list_find_value(_stash_list, j)
            var _data = ds_list_find_value(_item, 1)
            if (!ds_map_find_value_ext(_data, "identified", true))
            {
                var _record = ds_map_create()
                ds_map_add(_record, "stash_list", _stash_list)
                ds_map_add(_record, "index", j)
                ds_map_add(_record, "item", _item)
                ds_list_add(_unidentified_items, _record)
            }
        }
    }

    return _unidentified_items
}