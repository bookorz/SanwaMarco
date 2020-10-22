Function RobotSpeed();
    SETVAR("@device", "DNM01");
    SETVAR("@io", "@<arg1>");
    API("I7565DNM_REFRESH", True);
    API("I7565DNM_READIOS", True);	
    'API("I7565DNM_READIO", True);	
    'PRINT("I7565DNM_READIO_RETURN:@<I7565DNM_READIO_RETURN>");'DEBUG:檢查值
End Function;