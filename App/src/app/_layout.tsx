import { DarkTheme, DefaultTheme, ThemeProvider } from 'expo-router';
import { useColorScheme } from 'react-native';
import "~/global.css"
import { AnimatedSplashOverlay } from '@/components/animated-icon';
import AppTabs from '@/components/app-tabs';
import { PortalHost } from '@rn-primitives/portal';
import { NAV_THEME } from '../libs/theme';
import { SafeAreaProvider } from 'react-native-safe-area-context';


export default function TabLayout() {
  const colorScheme = useColorScheme();
  return (
    <SafeAreaProvider>
    <ThemeProvider value={NAV_THEME}>
      <AppTabs  />
      <PortalHost />
    </ThemeProvider>
    </SafeAreaProvider>
  );
}
