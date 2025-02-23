function mod_artknow_has_unidentified()
{
    with (o_inv_slot)
    {
        if (!ds_map_find_value_ext(data, "identified", true))
        {
            return true
        }
    }

    return false
}
