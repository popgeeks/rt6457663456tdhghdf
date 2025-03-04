
-- --------------------------------------------------------

--
-- Table structure for table `cfg_treasurehunter`
--

CREATE TABLE `cfg_treasurehunter` (
  `id` int(10) UNSIGNED NOT NULL,
  `ap` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `gold` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `card` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `jackpot` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `total` int(10) UNSIGNED NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `cfg_treasurehunter`
--

INSERT INTO `cfg_treasurehunter` (`id`, `ap`, `gold`, `card`, `jackpot`, `total`) VALUES
(1, 775000, 955000, 999000, 999960, 999999);
