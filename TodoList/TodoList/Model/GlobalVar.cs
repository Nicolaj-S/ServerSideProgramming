namespace TodoList.Model
{
    public class GlobalVar
    {
        public bool showAdmin { get; private set; } = false;

        public event Action OnChange;

        public void ShowAdminMenu()
        {
            showAdmin = true;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
