
-- --------------------------------------------------------

--
-- Table structure for table `treasurehunterlines`
--

CREATE TABLE `treasurehunterlines` (
  `id` int(10) UNSIGNED NOT NULL,
  `description` varchar(300) NOT NULL DEFAULT '',
  `type` varchar(5) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `treasurehunterlines`
--

INSERT INTO `treasurehunterlines` (`id`, `description`, `type`) VALUES
(1, 'You found sand.', '1'),
(2, 'You found dirt.', '1'),
(3, 'You found an empty bottle.', '1'),
(4, 'You found a rotten sandal.', '1'),
(5, 'You found nothing.', '1'),
(6, 'A crab pinches you as you hit it in the head with your shovel.', '1'),
(7, 'As time passes, you become too tired to dig and found nothing.', '1'),
(8, 'With a stroke of luck, you find a seashell.  You hold it up to your ear to listen to the whispering water only to find it is full of sand fleas.', '1'),
(9, 'During your excavation, you accidently step on a soft object.  Pain seers through your feet when you discover that you stepped on a scorpion.', '1'),
(10, 'You faint from exhaustion and disappointment in finding nothing.', '1'),
(11, 'You found a bottle full of a strange substance.', '2'),
(12, 'You found a seashell with some foreign money in it.', '2'),
(13, 'You found a fresh coconut.', '2'),
(14, 'You found a phoenix down.', '2'),
(15, 'You found a sand dollar.', '2'),
(16, 'You found 5 chocobo greens.', '3'),
(17, 'You found a bronze coin.', '3'),
(18, 'You grow weary as the night comes but as you walk back to your tent you step on a bottle shattering it.  Inside you find a crumpled up $20 bill.', '4'),
(19, 'You found a treasure chest full of bronze coins!', '4'),
(20, 'You found a treasure chest full of sapphires!', '5'),
(21, 'You found a treasure chest full of rubies!', '6'),
(22, 'As the seering sun pounds on your dehydrated head, you luckily find a crate full of spring water.', '6'),
(23, 'You are hungry and you decide to go try to catch a fish. ', '6'),
(24, 'You found a handful of shark\'s teeth. ', '6'),
(25, 'You found a treasure chest full of mixed variety of gemstones!  But alas no diamonds..', '7'),
(26, 'As you dig deeper into the hole you created, you hit a hard object with your shovel.  Suddenly, water comes from where you were digging, mixes with the sand, and sucks you into an abyss.', '1'),
(27, 'You found pirate bootay!', '4'),
(28, 'As you attempt to pick up a small shiny object, you are ambushed by local indians that you did not know were on this island.', '1'),
(30, 'You decided to dig in the water but forgot you cannot swim.  You drown.', '1'),
(31, 'Entrapped by this game, you forgot to shower.  Local crows decided to pick at you.  You die.', '1'),
(32, 'Surprisingly, you find a lost oyster.  Delighted, you rush to open the oyster only to find out that he only has a lump of coal in his mouth.', '1'),
(33, 'As you dig in the water of your treasure, you accidently slam your shovel in an oyster\'s head immediately killing it.  Inside you find a small pearl.', '2'),
(34, 'As you dig in the water of your treasure, you accidently slam your shovel in an oyster\'s head immediately killing it.  Inside you find a large pearl!', '5'),
(35, 'As you dig in the water for sunken treasure, you feel something biting your leg and then sudden bursts of pain.  As you run out of the water you notice pirannahs biting at you and demolishing your shovel.', '1'),
(36, 'Your shovel broke.', '1'),
(37, 'You found a kupo nut!', '2'),
(38, 'You found a diseased bananay.', '1'),
(39, 'You become tired while digging and decide to take a nature break.  You accidently pee on an electric fence and get a powerful shock.  Who puts an electric fence on an island anyway?', '1'),
(40, 'You caught a crab! But he pinches you forcing you to drop him and run like a little school girl.', '1');
