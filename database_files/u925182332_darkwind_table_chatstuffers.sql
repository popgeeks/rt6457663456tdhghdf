
-- --------------------------------------------------------

--
-- Table structure for table `chatstuffers`
--

CREATE TABLE `chatstuffers` (
  `id` int(10) UNSIGNED NOT NULL,
  `player` varchar(45) NOT NULL DEFAULT '',
  `active` int(10) UNSIGNED NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
