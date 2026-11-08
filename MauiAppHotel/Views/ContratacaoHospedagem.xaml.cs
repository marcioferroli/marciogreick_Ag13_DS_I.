
using MauiAppHotel.Models;

namespace MauiAppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    App PropriedadesApp;

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        PropriedadesApp = (App)Application.Current!;

        pck_quarto.ItemsSource = PropriedadesApp.lista_quartos;

        DateTime hoje = DateTime.Today;

        dtpck_checkin.MinimumDate = hoje;
        dtpck_checkin.MaximumDate = hoje.AddMonths(1);

        DateTime dataCheckIn = dtpck_checkin.Date ?? hoje;

        dtpck_checkout.MinimumDate = dataCheckIn.AddDays(1);
        dtpck_checkout.MaximumDate = dataCheckIn.AddMonths(6);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (pck_quarto.SelectedItem is not Quarto quarto)
            {
                await DisplayAlert("Atenção", "Selecione um quarto.", "OK");
                return;
            }

            DateTime dataCheckIn =
                dtpck_checkin.Date ?? DateTime.Today;

            DateTime dataCheckOut =
                dtpck_checkout.Date ?? dataCheckIn.AddDays(1);

            Hospedagem h = new Hospedagem
            {
                QuartoSelecionado = quarto,
                QntAdultos = Convert.ToInt32(stp_adultos.Value),
                QntCriancas = Convert.ToInt32(stp_criancas.Value),
                DataCheckIn = dataCheckIn,
                DataCheckOut = dataCheckOut
            };

            await Navigation.PushAsync(new HospedagemContratada()
            {
                BindingContext = h
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void dtpck_checkin_DateSelected(
        object sender, DateChangedEventArgs e)
    {
        DateTime dataSelecionada = e.NewDate ?? dtpck_checkin.Date ?? DateTime.Today;

        dtpck_checkout.MinimumDate = dataSelecionada.AddDays(1);
        dtpck_checkout.MaximumDate = dataSelecionada.AddMonths(6);
    }
}
