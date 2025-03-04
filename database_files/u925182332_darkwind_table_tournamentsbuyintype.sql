
-- --------------------------------------------------------

--
-- Table structure for table `tournamentsbuyintype`
--

CREATE TABLE `tournamentsbuyintype` (
  `tournamentsbuyintypeid` int(11) NOT NULL,
  `tournamentsbuyintypedescription` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `tournamentsbuyintype`
--

INSERT INTO `tournamentsbuyintype` (`tournamentsbuyintypeid`, `tournamentsbuyintypedescription`) VALUES
(1, 'GP'),
(2, 'AP');
