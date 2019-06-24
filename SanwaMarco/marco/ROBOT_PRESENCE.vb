Function RobotGetPrresence() AS String;

    SETVAR("@controller","$1");

    'Robot
    SETVAR("@RobotSensor1",?);
    SETVAR("@RobotSensor2",?);
    SETVAR("@RobotSensor3",?);
    API("CheckRobotSensor", true ,"ERR:99999999");
    RETURN("RobotGetPrresence @CheckRobotSensor");

End Function;