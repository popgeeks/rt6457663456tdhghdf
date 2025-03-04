
-- --------------------------------------------------------

--
-- Table structure for table `scratchgames`
--

CREATE TABLE `scratchgames` (
  `ID` int(11) NOT NULL,
  `GameName` varchar(50) NOT NULL,
  `TicketCost` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;

--
-- Dumping data for table `scratchgames`
--

INSERT INTO `scratchgames` (`ID`, `GameName`, `TicketCost`) VALUES
(1, 'Materia Magic', 100);
