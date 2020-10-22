Function O2_GET_STS();
    SETVAR("@device", "N2Purge");
	SETVAR("@cmd", "$1MCR:O2STS:1");
    API("N2_PURGE_GET_CMD", True);
    RETVAL("@<N2_PURGE_GET_CMD>");'回傳最終結果
End Function;