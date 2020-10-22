Function SET_RELIO();
    SETVAR("@device", "DNM01");
    SETVAR("@io", "@<arg1>");
    SETVAR("@value", "@<arg2>");
    API("I7565DNM_SETIO", True);
End Function;