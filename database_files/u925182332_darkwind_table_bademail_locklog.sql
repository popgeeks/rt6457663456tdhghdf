
-- --------------------------------------------------------

--
-- Table structure for table `bademail_locklog`
--

CREATE TABLE `bademail_locklog` (
  `id` int(10) UNSIGNED NOT NULL,
  `player` varchar(45) NOT NULL,
  `gold` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `ap` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `cards` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `email` varchar(45) NOT NULL,
  `datetime` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;
