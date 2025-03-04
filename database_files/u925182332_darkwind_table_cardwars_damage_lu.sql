
-- --------------------------------------------------------

--
-- Table structure for table `cardwars_damage_lu`
--

CREATE TABLE `cardwars_damage_lu` (
  `ID` int(10) UNSIGNED NOT NULL,
  `DamageTypeDescription` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `cardwars_damage_lu`
--

INSERT INTO `cardwars_damage_lu` (`ID`, `DamageTypeDescription`) VALUES
(1, 'Normal Damage'),
(2, 'Combo Damage'),
(3, 'Earth Damage'),
(4, 'Fire Damage'),
(5, 'Holy Damage'),
(6, 'Ice Damage'),
(7, 'Lightning Damage'),
(8, 'Mechanical Damage'),
(9, 'Poison Damage'),
(10, 'Shadow Damage'),
(11, 'Water Damage'),
(12, 'Wind Damage'),
(13, 'Healing');
