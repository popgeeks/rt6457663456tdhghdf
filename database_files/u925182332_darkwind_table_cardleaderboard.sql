
-- --------------------------------------------------------

--
-- Table structure for table `cardleaderboard`
--

CREATE TABLE `cardleaderboard` (
  `id` int(11) NOT NULL,
  `player` varchar(45) DEFAULT NULL,
  `cardname` varchar(50) DEFAULT NULL,
  `totalcards` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;
