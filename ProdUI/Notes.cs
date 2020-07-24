namespace ProdUI
{
    internal class Notes
    {
        /*

        RadFluidContentControl - allows for three sizes of content by using three different ContentControl templates
            supports custom transition animations / content switching can be triggered or left based on application size

        RadLayoutControl - allows arrangement of visual elements within its boundaries during runtime
            could be useful for displaying multiple arrangements of AC windows

        RadMulitColumnComboBox - definite for the selection of programs

        RadNavigationView (hamburger menu) could be useful?

        RadPersistenceFramework - allows saving and restoring UI - probably going to need this eventually

        https://github.com/PrismLibrary/Prism/pull/1682

        https://stackoverflow.com/questions/55865623/navigate-to-new-window-in-prism-wpf

        https://stackoverflow.com/questions/34125982/prism-pop-up-new-window-in-wpf

        https://stackoverflow.com/questions/7800032/cancel-combobox-selection-in-wpf-with-mvvm



        <ControlTemplate x:Key="CarouselDataFieldPresenterTemplate" TargetType="{x:Type telerikNavigation:CarouselDataFieldPresenter}">
        <Border Margin="7"
                Background="Transparent"
                BorderBrush="{TemplateBinding BorderBrush}"
                BorderThickness="{TemplateBinding BorderThickness}">
            <Grid>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition MinWidth="105" />
                    <ColumnDefinition Width="*" />
                    <ColumnDefinition MinWidth="105" />
                </Grid.ColumnDefinitions>
                <TextBlock HorizontalAlignment="Left"
                           VerticalAlignment="Center"
                           Text="{TemplateBinding Header}"
                           TextElement.FontWeight="Bold" />
                <ContentPresenter Grid.Column="2"
                                  HorizontalAlignment="Left"
                                  VerticalAlignment="Center"
                                  Content="{TemplateBinding Content}">
                    <ContentPresenter.ContentTemplate>
                        <DataTemplate>
                            <TextBlock Text="{Binding}" TextTrimming="None" TextWrapping="Wrap" />
                        </DataTemplate>
                    </ContentPresenter.ContentTemplate>
                </ContentPresenter>
            </Grid>
        </Border>
    </ControlTemplate>
    <Style x:Key="CarouselDataFieldPresenterStyle" TargetType="{x:Type telerikNavigation:CarouselDataFieldPresenter}">
        <Setter Property="FocusVisualStyle" Value="{x:Null}" />
        <Setter Property="Template" Value="{StaticResource CarouselDataFieldPresenterTemplate}" />
    </Style>
    <Style BasedOn="{StaticResource CarouselDataFieldPresenterStyle}" TargetType="{x:Type telerikNavigation:CarouselDataFieldPresenter}" />
    <ControlTemplate x:Key="CarouselItemScrollViewerTemplate" TargetType="{x:Type telerikNavigation:CarouselScrollViewer}">
        <Grid Background="Transparent">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="Auto" />
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="*" />
                <RowDefinition Height="Auto" />
            </Grid.RowDefinitions>
            <ScrollContentPresenter x:Name="PART_ScrollContentPresenter"
                                    Margin="{TemplateBinding Padding}"
                                    CanContentScroll="{TemplateBinding CanContentScroll}"
                                    CanHorizontallyScroll="False"
                                    CanVerticallyScroll="False"
                                    Content="{TemplateBinding Content}"
                                    ContentTemplate="{TemplateBinding ContentTemplate}" />
            <ScrollBar x:Name="PART_VerticalScrollBar"
                       Grid.Row="0"
                       Grid.Column="1"
                       AutomationProperties.AutomationId="VerticalScrollBar"
                       Cursor="Arrow"
                       Maximum="{TemplateBinding ScrollableHeight}"
                       Minimum="0"
                       ViewportSize="{TemplateBinding ViewportHeight}"
                       Visibility="{TemplateBinding ComputedVerticalScrollBarVisibility}"
                       Value="{Binding Path=VerticalOffset, Mode=OneWay, RelativeSource={RelativeSource TemplatedParent}}" />
            <ScrollBar x:Name="PART_HorizontalScrollBar"
                       Grid.Row="1"
                       Grid.Column="0"
                       AutomationProperties.AutomationId="HorizontalScrollBar"
                       Cursor="Arrow"
                       Maximum="{TemplateBinding ScrollableWidth}"
                       Minimum="0"
                       Orientation="Horizontal"
                       ViewportSize="{TemplateBinding ViewportWidth}"
                       Visibility="{TemplateBinding ComputedHorizontalScrollBarVisibility}"
                       Value="{Binding Path=HorizontalOffset, Mode=OneWay, RelativeSource={RelativeSource TemplatedParent}}" />
        </Grid>
    </ControlTemplate>
    <ControlTemplate x:Key="CarouselDataRecordPresenterTemplate" TargetType="{x:Type telerikNavigation:CarouselDataRecordPresenter}">
        <telerikNavigation:CarouselScrollViewer Template="{StaticResource CarouselItemScrollViewerTemplate}" VerticalScrollBarVisibility="Auto">
            <ItemsControl Background="Transparent" ClipToBounds="True" ItemsSource="{Binding Path=FieldDescriptors, RelativeSource={RelativeSource AncestorType={x:Type telerikNavigation:RadCarousel}}}">
                <ItemsControl.ItemTemplate>
                    <DataTemplate>
                        <telerikNavigation:CarouselDataFieldPresenter Data="{Binding DataContext, RelativeSource={RelativeSource AncestorType=telerikNavigation:CarouselDataRecordPresenter}}" />
                    </DataTemplate>
                </ItemsControl.ItemTemplate>
            </ItemsControl>
        </telerikNavigation:CarouselScrollViewer>
    </ControlTemplate>
    <Style x:Key="CarouselDataRecordPresenterStyle" TargetType="{x:Type telerikNavigation:CarouselDataRecordPresenter}">
        <Setter Property="FocusVisualStyle" Value="{x:Null}" />
        <Setter Property="SnapsToDevicePixels" Value="True" />
        <Setter Property="Template" Value="{StaticResource CarouselDataRecordPresenterTemplate}" />
    </Style>
    <Style BasedOn="{StaticResource CarouselDataRecordPresenterStyle}" TargetType="{x:Type telerikNavigation:CarouselDataRecordPresenter}" />
    <carousel:ArithmeticValueConverter x:Key="ArithmeticValueConverter" />
    <ControlTemplate x:Key="CarouselItemTemplate" TargetType="{x:Type telerikNavigation:CarouselItem}">
        <Grid Width="{TemplateBinding Width}" Height="{TemplateBinding Height}" ClipToBounds="False">
            <Border x:Name="reflection"
                    Width="{Binding ElementName=dialog_Copy, Path=ActualWidth}"
                    BorderBrush="White"
                    BorderThickness="0"
                    ClipToBounds="False"
                    IsHitTestVisible="False"
                    Opacity="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=ReflectionSettings.Opacity}"
                    RenderTransformOrigin="0.5 1"
                    Visibility="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=ReflectionSettings.Visibility}">
                <Border.RenderTransform>
                    <TransformGroup>
                        <ScaleTransform x:Name="scaleTransform" CenterY="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=ReflectionSettings.OffsetY}" ScaleX="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=ReflectionSettings.WidthOffset, Converter={StaticResource ArithmeticValueConverter}, ConverterParameter=1}" ScaleY="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=ReflectionSettings.HeightOffset, Converter={StaticResource ArithmeticValueConverter}, ConverterParameter=-1}" />
                        <TranslateTransform x:Name="translateTransform" X="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=ReflectionSettings.OffsetX}" />
                        <SkewTransform x:Name="skewTransform" AngleX="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=ReflectionSettings.Angle}" />
                    </TransformGroup>
                </Border.RenderTransform>
                <Border.Background>
                    <VisualBrush x:Name="visualBrush" Visual="{Binding ElementName=dialog_Copy}" />
                </Border.Background>
            </Border>
            <Border x:Name="dialog_Copy">
                <Grid>
                    <mat:Shadow Background="{TemplateBinding Background}" ShadowDepth="{TemplateBinding mat:MaterialAssist.ShadowDepth}" />
                    <Border x:Name="CarouselItemBackground"
                            Background="{TemplateBinding Background}"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}">
                        <ContentPresenter x:Name="CarouselContentPresenter"
                                          Margin="{TemplateBinding Padding}"
                                          HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                                          VerticalAlignment="{TemplateBinding VerticalContentAlignment}" />
                    </Border>
                    <Rectangle x:Name="SelectedRectangle"
                               Height="3"
                               HorizontalAlignment="Stretch"
                               VerticalAlignment="Bottom"
                               Fill="{TemplateBinding mat:MaterialAssist.FocusBrush}"
                               RenderTransformOrigin=".5 1"
                               Stroke="{x:Null}"
                               StrokeThickness="0">
                        <Rectangle.RenderTransform>
                            <ScaleTransform ScaleX="0" ScaleY="1" />
                        </Rectangle.RenderTransform>
                    </Rectangle>
                </Grid>
            </Border>
        </Grid>
        <ControlTemplate.Triggers>
            <Trigger Property="IsSelected" Value="True">
                <Setter Property="Background" Value="{Binding RelativeSource={RelativeSource Self}, Path=(mat:MaterialAssist.CheckedBrush)}" />
            </Trigger>

            <!--  IsSelected and animation disabled  -->
            <MultiTrigger>
                <MultiTrigger.Conditions>
                    <Condition Property="IsSelected" Value="True" />
                    <Condition Property="animation:AnimationManager.IsAnimationEnabled" Value="False" />
                </MultiTrigger.Conditions>
                <Setter TargetName="CarouselItemBackground" Property="BorderBrush" Value="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=(mat:MaterialAssist.FocusBrush), Mode=OneWay}" />
                <Setter TargetName="CarouselItemBackground" Property="BorderThickness" Value="0,0,0,3" />
            </MultiTrigger>

            <!--  IsSelected and animation Enabled  -->
            <MultiTrigger>
                <MultiTrigger.Conditions>
                    <Condition Property="IsSelected" Value="True" />
                    <Condition Property="animation:AnimationManager.IsAnimationEnabled" Value="True" />
                </MultiTrigger.Conditions>
                <MultiTrigger.EnterActions>
                    <BeginStoryboard>
                        <Storyboard>
                            <DoubleAnimationUsingKeyFrames Storyboard.TargetName="SelectedRectangle" Storyboard.TargetProperty="RenderTransform.ScaleX">
                                <EasingDoubleKeyFrame KeyTime="0" Value="0" />
                                <EasingDoubleKeyFrame KeyTime="0:0:0.12" Value="1">
                                    <EasingDoubleKeyFrame.EasingFunction>
                                        <SineEase EasingMode="EaseIn" />
                                    </EasingDoubleKeyFrame.EasingFunction>
                                </EasingDoubleKeyFrame>
                            </DoubleAnimationUsingKeyFrames>
                        </Storyboard>
                    </BeginStoryboard>
                </MultiTrigger.EnterActions>
                <MultiTrigger.ExitActions>
                    <BeginStoryboard>
                        <Storyboard>
                            <DoubleAnimationUsingKeyFrames Storyboard.TargetName="SelectedRectangle" Storyboard.TargetProperty="RenderTransform.ScaleX">
                                <EasingDoubleKeyFrame KeyTime="0" Value="1" />
                                <EasingDoubleKeyFrame KeyTime="0:0:0.1" Value="0">
                                    <EasingDoubleKeyFrame.EasingFunction>
                                        <SineEase EasingMode="EaseIn" />
                                    </EasingDoubleKeyFrame.EasingFunction>
                                </EasingDoubleKeyFrame>
                            </DoubleAnimationUsingKeyFrames>
                        </Storyboard>
                    </BeginStoryboard>
                </MultiTrigger.ExitActions>
            </MultiTrigger>
        </ControlTemplate.Triggers>
    </ControlTemplate>
    <Style x:Key="CarouselItemStyle" TargetType="{x:Type telerikNavigation:CarouselItem}">
        <Setter Property="Background" Value="{telerik1:MaterialResource ResourceKey=PrimaryBrush}" />
        <Setter Property="BorderBrush" Value="{telerik1:MaterialResource ResourceKey=DividerBrush}" />
        <Setter Property="BorderThickness" Value="0" />
        <Setter Property="FocusVisualStyle" Value="{x:Null}" />
        <Setter Property="Foreground" Value="{telerik1:MaterialResource ResourceKey=MarkerBrush}" />
        <Setter Property="HorizontalContentAlignment" Value="Left" />
        <Setter Property="Padding" Value="8" />
        <Setter Property="Template" Value="{StaticResource CarouselItemTemplate}" />
        <Setter Property="VerticalContentAlignment" Value="Top" />
        <Setter Property="mat:MaterialAssist.CheckedBrush" Value="{telerik1:MaterialResource ResourceKey=AlternativeBrush}" />
        <Setter Property="mat:MaterialAssist.FocusBrush" Value="{telerik1:MaterialResource ResourceKey=PrimaryPressedBrush}" />
        <Setter Property="mat:MaterialAssist.ShadowDepth" Value="Depth1" />
    </Style>
    <Style BasedOn="{StaticResource CarouselItemStyle}" TargetType="{x:Type telerikNavigation:CarouselItem}" />
    <DataTemplate x:Key="AutoGeneratedItemContentTemplate">
        <telerikNavigation:CarouselDataRecordPresenter />
    </DataTemplate>
    <DataTemplate x:Key="DefaultItemContentPresenterTemplate">
        <ContentPresenter Content="{Binding}" />
    </DataTemplate>
    <telerikNavigation:CarouselItemContentTemplateSelector x:Key="CarouselItemContentTemplateSelector" />
    <Style x:Key="CarouselItemContentPresenterStyle" TargetType="{x:Type telerikNavigation:CarouselItemContentPresenter}">
        <Setter Property="AutoGeneratedItemContentTemplate" Value="{StaticResource AutoGeneratedItemContentTemplate}" />
        <Setter Property="DefaultItemContentPresenterTemplate" Value="{StaticResource DefaultItemContentPresenterTemplate}" />
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="ContentControl">
                    <ContentPresenter Content="{Binding}" ContentTemplateSelector="{StaticResource CarouselItemContentTemplateSelector}" />
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>
    <Style BasedOn="{StaticResource CarouselItemContentPresenterStyle}" TargetType="{x:Type telerikNavigation:CarouselItemContentPresenter}" />
    <ControlTemplate x:Key="CarouselScrollButtonTemplate" TargetType="{x:Type carousel:CarouselScrollButton}">
        <Grid>
            <mat:Shadow Background="{TemplateBinding Background}" CornerRadius="{TemplateBinding mat:MaterialAssist.CornerRadius}" ShadowDepth="{TemplateBinding mat:MaterialAssist.ShadowDepth}" />
            <Border x:Name="Arrow"
                    Width="{TemplateBinding Width}"
                    Height="{TemplateBinding Height}"
                    Background="{TemplateBinding Background}"
                    BorderBrush="{TemplateBinding BorderBrush}"
                    BorderThickness="{TemplateBinding BorderThickness}"
                    CornerRadius="{TemplateBinding mat:MaterialAssist.CornerRadius}"
                    SnapsToDevicePixels="True"
                    UseLayoutRounding="True">
                <mat:MaterialControl CornerRadius="{TemplateBinding mat:MaterialAssist.CornerRadius}" IsSmartClipped="True">
                    <Grid HorizontalAlignment="Center" VerticalAlignment="Center" Background="Transparent">
                        <TextBlock x:Name="Glyph"
                                   HorizontalAlignment="Center"
                                   VerticalAlignment="Center"
                                   FontFamily="{StaticResource TelerikWebUI}"
                                   FontSize="16"
                                   FontStyle="Normal"
                                   FontWeight="Normal"
                                   Foreground="{TemplateBinding Foreground}"
                                   Opacity="{telerik1:MaterialResource ResourceKey=PrimaryOpacity}">
                            <Run x:Name="GlyphRun" Text="{x:Null}" />
                        </TextBlock>
                    </Grid>
                </mat:MaterialControl>
            </Border>
        </Grid>
        <ControlTemplate.Triggers>
            <Trigger Property="IsKeyboardFocused" Value="True">
                <Setter Property="Foreground" Value="{telerik1:MaterialResource ResourceKey=IconBrush}" />
                <Setter Property="mat:MaterialAssist.ShadowDepth" Value="Depth3" />
                <Setter TargetName="Arrow" Property="Background" Value="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=(mat:MaterialAssist.FocusBrush)}" />
            </Trigger>
            <Trigger Property="IsMouseOver" Value="True">
                <Setter Property="Foreground" Value="{telerik1:MaterialResource ResourceKey=MarkerInvertedBrush}" />
                <Setter Property="mat:MaterialAssist.ShadowDepth" Value="Depth2" />
                <Setter TargetName="Glyph" Property="Opacity" Value="1" />
            </Trigger>
            <Trigger Property="IsPressed" Value="True">
                <Setter Property="Foreground" Value="{telerik1:MaterialResource ResourceKey=MarkerInvertedBrush}" />
                <Setter Property="mat:MaterialAssist.ShadowDepth" Value="Depth3" />
                <Setter TargetName="Glyph" Property="Opacity" Value="1" />
            </Trigger>
            <Trigger Property="IsEnabled" Value="False">
                <Setter TargetName="Glyph" Property="Opacity" Value="{telerik1:MaterialResource ResourceKey=DisabledOpacity}" />
            </Trigger>
            <Trigger Property="CarouselScrollButtonType" Value="LineUp">
                <Setter TargetName="GlyphRun" Property="Text" Value="{StaticResource GlyphArrow60Up}" />
            </Trigger>
            <Trigger Property="CarouselScrollButtonType" Value="LineDown">
                <Setter TargetName="GlyphRun" Property="Text" Value="{StaticResource GlyphArrow60Down}" />
            </Trigger>
            <Trigger Property="CarouselScrollButtonType" Value="LineLeft">
                <Setter TargetName="GlyphRun" Property="Text" Value="{StaticResource GlyphArrow60Left}" />
            </Trigger>
            <Trigger Property="CarouselScrollButtonType" Value="LineRight">
                <Setter TargetName="GlyphRun" Property="Text" Value="{StaticResource GlyphArrow60Right}" />
            </Trigger>
            <Trigger Property="CarouselScrollButtonType" Value="PageUp">
                <Setter TargetName="GlyphRun" Property="Text" Value="{StaticResource GlyphArrowUpward}" />
            </Trigger>
            <Trigger Property="CarouselScrollButtonType" Value="PageDown">
                <Setter TargetName="GlyphRun" Property="Text" Value="{StaticResource GlyphArrowDownward}" />
            </Trigger>
            <Trigger Property="CarouselScrollButtonType" Value="PageLeft">
                <Setter TargetName="GlyphRun" Property="Text" Value="{StaticResource GlyphArrowBackward}" />
            </Trigger>
            <Trigger Property="CarouselScrollButtonType" Value="PageRight">
                <Setter TargetName="GlyphRun" Property="Text" Value="{StaticResource GlyphArrowForward}" />
            </Trigger>
            <MultiTrigger>
                <MultiTrigger.Conditions>
                    <Condition Property="CarouselScrollButtonType" Value="LineLeft" />
                    <Condition Property="FlowDirection" Value="RightToLeft" />
                </MultiTrigger.Conditions>
                <Setter TargetName="GlyphRun" Property="Text" Value="{StaticResource GlyphArrow60Right}" />
            </MultiTrigger>
            <MultiTrigger>
                <MultiTrigger.Conditions>
                    <Condition Property="CarouselScrollButtonType" Value="LineRight" />
                    <Condition Property="FlowDirection" Value="RightToLeft" />
                </MultiTrigger.Conditions>
                <Setter TargetName="GlyphRun" Property="Text" Value="{StaticResource GlyphArrow60Left}" />
            </MultiTrigger>
            <MultiTrigger>
                <MultiTrigger.Conditions>
                    <Condition Property="CarouselScrollButtonType" Value="PageLeft" />
                    <Condition Property="FlowDirection" Value="RightToLeft" />
                </MultiTrigger.Conditions>
                <Setter TargetName="GlyphRun" Property="Text" Value="{StaticResource GlyphArrowForward}" />
            </MultiTrigger>
            <MultiTrigger>
                <MultiTrigger.Conditions>
                    <Condition Property="CarouselScrollButtonType" Value="PageRight" />
                    <Condition Property="FlowDirection" Value="RightToLeft" />
                </MultiTrigger.Conditions>
                <Setter TargetName="GlyphRun" Property="Text" Value="{StaticResource GlyphArrowBackward}" />
            </MultiTrigger>
        </ControlTemplate.Triggers>
    </ControlTemplate>
    <Style x:Key="CarouselScrollButtonStyle" TargetType="{x:Type carousel:CarouselScrollButton}">
        <Setter Property="Background" Value="{telerik1:MaterialResource ResourceKey=MainBrush}" />
        <Setter Property="BorderBrush" Value="{telerik1:MaterialResource ResourceKey=DividerBrush}" />
        <Setter Property="BorderThickness" Value="0" />
        <Setter Property="FocusVisualStyle" Value="{x:Null}" />
        <Setter Property="Foreground" Value="{telerik1:MaterialResource ResourceKey=IconBrush}" />
        <Setter Property="Height" Value="36" />
        <Setter Property="HorizontalContentAlignment" Value="Center" />
        <Setter Property="Template" Value="{StaticResource CarouselScrollButtonTemplate}" />
        <Setter Property="VerticalContentAlignment" Value="Center" />
        <Setter Property="Width" Value="36" />
        <Setter Property="mat:MaterialAssist.CornerRadius" Value="{telerik1:MaterialResource ResourceKey=CornerRadius}" />
        <Setter Property="mat:MaterialAssist.FocusBrush" Value="{telerik1:MaterialResource ResourceKey=PrimaryFocusBrush}" />
        <Setter Property="mat:MaterialAssist.MouseOverBrush" Value="{telerik1:MaterialResource ResourceKey=PrimaryHoverBrush}" />
        <Setter Property="mat:MaterialAssist.PressedBrush" Value="{telerik1:MaterialResource ResourceKey=PrimaryPressedBrush}" />
        <Setter Property="mat:MaterialAssist.ShadowDepth" Value="Depth1" />
    </Style>
    <Style BasedOn="{StaticResource CarouselScrollButtonStyle}" TargetType="{x:Type carousel:CarouselScrollButton}" />
    <ControlTemplate x:Key="CarouselVerticalScrollBarTemplate" TargetType="{x:Type ScrollBar}">
        <Grid x:Name="Bg"
              VerticalAlignment="Center"
              Background="Transparent"
              SnapsToDevicePixels="true">
            <Grid.RowDefinitions>
                <RowDefinition Height="{TemplateBinding Height}" />
                <RowDefinition Height="{TemplateBinding Height}" />
                <RowDefinition Height="{TemplateBinding Height}" />
                <RowDefinition Height="{TemplateBinding Height}" />
            </Grid.RowDefinitions>
            <carousel:CarouselScrollButton Grid.Row="0"
                                           Margin="4"
                                           CarouselScrollButtonType="PageUp"
                                           Command="{x:Static ScrollBar.PageUpCommand}" />
            <carousel:CarouselScrollButton Grid.Row="1"
                                           Margin="4"
                                           CarouselScrollButtonType="LineUp"
                                           Command="{x:Static ScrollBar.LineUpCommand}" />
            <carousel:CarouselScrollButton Grid.Row="2"
                                           Margin="4"
                                           CarouselScrollButtonType="LineDown"
                                           Command="{x:Static ScrollBar.LineDownCommand}" />
            <carousel:CarouselScrollButton Grid.Row="3"
                                           Margin="4"
                                           CarouselScrollButtonType="PageDown"
                                           Command="{x:Static ScrollBar.PageDownCommand}" />
        </Grid>
    </ControlTemplate>
    <Style x:Key="CarouselVerticalScrollBarStyle" TargetType="ScrollBar">
        <Setter Property="Background" Value="Transparent" />
        <Setter Property="MinHeight" Value="44" />
        <Setter Property="Template" Value="{StaticResource CarouselVerticalScrollBarTemplate}" />
        <Setter Property="Width" Value="44" />
    </Style>
    <ControlTemplate x:Key="CarouselHorizontalScrollBarTemplate" TargetType="{x:Type ScrollBar}">
        <Grid x:Name="Bg"
              HorizontalAlignment="Center"
              Background="{TemplateBinding Background}"
              SnapsToDevicePixels="true">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="{TemplateBinding Width}" />
                <ColumnDefinition Width="{TemplateBinding Width}" />
                <ColumnDefinition Width="{TemplateBinding Width}" />
                <ColumnDefinition Width="{TemplateBinding Width}" />
            </Grid.ColumnDefinitions>
            <carousel:CarouselScrollButton Grid.Column="0"
                                           Margin="4"
                                           CarouselScrollButtonType="PageLeft"
                                           Command="{x:Static ScrollBar.PageUpCommand}"
                                           Style="{StaticResource CarouselScrollButtonStyle}" />
            <carousel:CarouselScrollButton Grid.Column="1"
                                           Margin="4"
                                           CarouselScrollButtonType="LineLeft"
                                           Command="{x:Static ScrollBar.LineLeftCommand}"
                                           Style="{StaticResource CarouselScrollButtonStyle}" />
            <carousel:CarouselScrollButton Grid.Column="2"
                                           Margin="4"
                                           CarouselScrollButtonType="LineRight"
                                           Command="{x:Static ScrollBar.LineRightCommand}"
                                           Style="{StaticResource CarouselScrollButtonStyle}" />
            <carousel:CarouselScrollButton Grid.Column="3"
                                           Margin="4"
                                           CarouselScrollButtonType="PageRight"
                                           Command="{x:Static ScrollBar.PageDownCommand}"
                                           Style="{StaticResource CarouselScrollButtonStyle}" />
        </Grid>
    </ControlTemplate>
    <Style x:Key="CarouselHorizontalScrollBarStyle" TargetType="ScrollBar">
        <Setter Property="Background" Value="Transparent" />
        <Setter Property="Height" Value="44" />
        <Setter Property="MinWidth" Value="44" />
        <Setter Property="Template" Value="{StaticResource CarouselHorizontalScrollBarTemplate}" />
    </Style>
    <ControlTemplate x:Key="CarouselScrollViewerTemplate" TargetType="{x:Type telerikNavigation:CarouselScrollViewer}">
        <Grid x:Name="Grid" Background="{TemplateBinding Background}">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="Auto" />
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="*" />
                <RowDefinition Height="Auto" />
            </Grid.RowDefinitions>
            <ScrollContentPresenter x:Name="PART_ScrollContentPresenter"
                                    Grid.Row="0"
                                    Grid.Column="0"
                                    Margin="{TemplateBinding Padding}"
                                    CanContentScroll="{TemplateBinding CanContentScroll}"
                                    CanHorizontallyScroll="False"
                                    CanVerticallyScroll="False"
                                    Content="{TemplateBinding Content}"
                                    ContentTemplate="{TemplateBinding ContentTemplate}" />
            <ScrollBar x:Name="PART_VerticalScrollBar"
                       Grid.Row="0"
                       Grid.Column="1"
                       AutomationProperties.AutomationId="VerticalScrollBar"
                       Cursor="Arrow"
                       Maximum="{TemplateBinding ScrollableHeight}"
                       Minimum="0"
                       Style="{StaticResource CarouselVerticalScrollBarStyle}"
                       ViewportSize="{TemplateBinding ViewportHeight}"
                       Visibility="{TemplateBinding ComputedVerticalScrollBarVisibility}"
                       Value="{Binding Path=VerticalOffset, Mode=OneWay, RelativeSource={RelativeSource TemplatedParent}}" />
            <ScrollBar x:Name="PART_HorizontalScrollBar"
                       Grid.Row="1"
                       Grid.Column="0"
                       AutomationProperties.AutomationId="HorizontalScrollBar"
                       Cursor="Arrow"
                       Maximum="{TemplateBinding ScrollableWidth}"
                       Minimum="0"
                       Orientation="Horizontal"
                       Style="{StaticResource CarouselHorizontalScrollBarStyle}"
                       ViewportSize="{TemplateBinding ViewportWidth}"
                       Visibility="{TemplateBinding ComputedHorizontalScrollBarVisibility}"
                       Value="{Binding Path=HorizontalOffset, Mode=OneWay, RelativeSource={RelativeSource TemplatedParent}}" />
        </Grid>
    </ControlTemplate>
    <ItemsPanelTemplate x:Key="CarouselItemsControlItemPanelTemplate">
        <telerikNavigation:RadCarouselPanel x:Name="CarouselPanel" IsPathVisible="False" />
    </ItemsPanelTemplate>
    <ControlTemplate x:Key="CarouselItemsControlTemplate" TargetType="{x:Type carousel:CarouselItemsControl}">
        <telerikNavigation:CarouselScrollViewer CanContentScroll="True"
                                                HorizontalScrollBarVisibility="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type telerikNavigation:RadCarousel}}, Path=HorizontalScrollBarVisibility}"
                                                Template="{StaticResource CarouselScrollViewerTemplate}"
                                                VerticalScrollBarVisibility="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type telerikNavigation:RadCarousel}}, Path=VerticalScrollBarVisibility}">
            <ItemsPresenter x:Name="ItemsPresenter" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" />
        </telerikNavigation:CarouselScrollViewer>
    </ControlTemplate>
    <Style x:Key="CarouselItemsControlStyle" TargetType="{x:Type carousel:CarouselItemsControl}">
        <Setter Property="FocusVisualStyle" Value="{x:Null}" />
        <Setter Property="ItemTemplate">
            <Setter.Value>
                <DataTemplate>
                    <telerikNavigation:CarouselItemContentPresenter />
                </DataTemplate>
            </Setter.Value>
        </Setter>
        <Setter Property="ItemsPanel" Value="{StaticResource CarouselItemsControlItemPanelTemplate}" />
        <Setter Property="Template" Value="{StaticResource CarouselItemsControlTemplate}" />
    </Style>
    <Style BasedOn="{StaticResource CarouselItemsControlStyle}" TargetType="{x:Type carousel:CarouselItemsControl}" />
    <Style x:Key="CarouselPanelStyle" TargetType="{x:Type telerikNavigation:RadCarouselPanel}">
        <Setter Property="Background" Value="Transparent" />
    </Style>
    <Style BasedOn="{StaticResource CarouselPanelStyle}" TargetType="{x:Type telerikNavigation:RadCarouselPanel}" />
    <ControlTemplate x:Key="RadCarouselTemplate" TargetType="{x:Type telerikNavigation:RadCarousel}">
        <Border Background="{TemplateBinding Background}" BorderBrush="{TemplateBinding BorderBrush}" BorderThickness="{TemplateBinding BorderThickness}">
            <carousel:CarouselItemsControl x:Name="PART_RootItemsControl" Margin="{TemplateBinding Padding}" />
        </Border>
    </ControlTemplate>
    <Style x:Key="RadCarouselStyle" TargetType="{x:Type telerikNavigation:RadCarousel}">
        <Setter Property="ClipToBounds" Value="True" />
        <Setter Property="FocusVisualStyle" Value="{x:Null}" />
        <Setter Property="FontFamily" Value="{telerik1:MaterialResource ResourceKey=FontFamily}" />
        <Setter Property="FontSize" Value="{telerik1:MaterialResource ResourceKey=FontSize}" />
        <Setter Property="SnapsToDevicePixels" Value="True" />
        <Setter Property="Template" Value="{StaticResource RadCarouselTemplate}" />
    </Style>
    <Style BasedOn="{StaticResource RadCarouselStyle}" TargetType="{x:Type telerikNavigation:RadCarousel}" />




        */
    }
}