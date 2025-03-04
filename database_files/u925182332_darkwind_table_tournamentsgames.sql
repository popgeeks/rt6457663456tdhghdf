
-- --------------------------------------------------------

--
-- Table structure for table `tournamentsgames`
--

CREATE TABLE `tournamentsgames` (
  `tournamentsgamesid` int(10) UNSIGNED NOT NULL,
  `tournamentsgamesdescription` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `tournamentsgames`
--

INSERT INTO `tournamentsgames` (`tournamentsgamesid`, `tournamentsgamesdescription`) VALUES
(1, 'Omni Triple Triad - Random Deck/5');
