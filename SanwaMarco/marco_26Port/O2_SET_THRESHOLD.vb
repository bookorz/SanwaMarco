Function O2_SET_THRESHOLD();
    SETVAR("@device", "N2Purge");
    '<arg1> : THRESHOLD
	SETVAR("@cmd", "$1MCR:O2THR:1,@<arg1>,@<arg2>");
    API("N2_PURGE_SET_CMD", True);
End Function;