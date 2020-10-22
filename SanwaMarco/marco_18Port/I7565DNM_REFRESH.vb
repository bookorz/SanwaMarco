Function DNM_REFRESH();
    SETVAR("@device", "DNM01");
    API("I7565DNM_REFRESH", True);
End Function;