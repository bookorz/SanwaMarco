Function N2PURGE_INIT();
    SETVAR("@device", "N2Purge");
    '初始化
	SETVAR("@cmd", "$1MCR:INIT_:1");
    API("N2_PURGE_SET_CMD", True);
End Function;