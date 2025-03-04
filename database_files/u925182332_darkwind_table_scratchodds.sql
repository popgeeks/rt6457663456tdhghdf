
-- --------------------------------------------------------

--
-- Table structure for table `scratchodds`
--

CREATE TABLE `scratchodds` (
  `ID` int(11) NOT NULL,
  `ScratchGameID` int(11) NOT NULL,
  `OddsDescription` varchar(50) NOT NULL,
  `OddsValue` int(11) NOT NULL,
  `OddsMultiplier` int(11) NOT NULL,
  `IsJackpot` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;

--
-- Dumping data for table `scratchodds`
--

INSERT INTO `scratchodds` (`ID`, `ScratchGameID`, `OddsDescription`, `OddsValue`, `OddsMultiplier`, `IsJackpot`) VALUES
(1, 1, 'Magic', 3, 1, 0),
(2, 1, 'Compound', 9, 3, 0),
(3, 1, 'Independant', 16, 5, 0),
(4, 1, 'Summon', 60, 25, 0),
(5, 1, 'Support', 120, 50, 0),
(6, 1, 'Meteor', 300, 100, 0),
(7, 1, 'Holy', 1100, 500, 0),
(8, 1, 'Moogle', 50000, 1, 1);
