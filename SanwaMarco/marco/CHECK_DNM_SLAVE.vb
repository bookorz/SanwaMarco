Function CHECK_DNM_SLAVE();
    SETVAR("@device", "DNM01");
    API("I7565DNM_CHECK_SLAVE", True);
End Function;