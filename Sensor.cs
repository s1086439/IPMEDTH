﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface Sensor: Hardware
{
	float X { get; set; }
	float Y { get; set; }
	float Z { get; set; }
}